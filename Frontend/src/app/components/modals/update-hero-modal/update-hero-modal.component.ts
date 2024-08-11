import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-hero-modal',
  templateUrl: './update-hero-modal.component.html',
  styleUrls: ['./update-hero-modal.component.scss']
})
export class UpdateHeroModalComponent {
  heroForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<UpdateHeroModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {

    this.heroForm = this.fb.group({
      id: [data.hero.id],
      nome: [data.hero.nome, Validators.required],
      nomeHeroi: [data.hero.nomeHeroi, Validators.required],
      dataNascimento: [data.hero.dataNascimento, Validators.required],
      altura: [data.hero.altura, Validators.required],
      peso: [data.hero.peso, Validators.required],
      superpoderes: this.buildSuperpowersArray(data.superpowers, data.hero.superpoderes)
    });
  }

  createSuperpoderesControls(superpowers: any[], heroSuperpowers: any[]): any[] {
    return superpowers.map(sp => this.fb.control(heroSuperpowers.includes(sp.superpoderNome)));
  }

  get superpoderesArray(): FormArray {
    return this.heroForm.get('superpoderes') as FormArray;
  }

  buildSuperpowersArray(superpowers: any[], heroSuperpowers: any[]): FormArray {
    const arr = superpowers.map(sp => {
      const isSelected = heroSuperpowers.some(heroSp => heroSp.superpoderNome === sp.superpoderNome);
      return this.fb.control(isSelected);
    });
    return this.fb.array(arr);
  }
  onNoClick(): void {
    this.dialogRef.close();
  }

  onSaveClick(): void {
    if (this.heroForm.valid) {
      // Mapeando os superpoderes selecionados
      const selectedSuperpowers = this.heroForm.value.superpoderes
        .map((checked: boolean, index: number) => {
          // Se o checkbox está marcado, retornamos o objeto do superpoder
          return checked ? this.data.superpowers[index] : null;
        })
        .filter((superpower: any) => superpower !== null); // Filtrando os que foram selecionados
      if (selectedSuperpowers.length === 0) {
        // Alerta se nenhum superpoder foi selecionado
        alert("Selecione pelo menos um superpoder.");
        return;
      }

      // Criando o herói atualizado com os superpoderes selecionados
      const updatedHero = {
        ...this.heroForm.value,
        superpoderes: selectedSuperpowers // Substituindo superpoderes booleanos por objetos de superpoderes
      };

      // Fechando o modal e retornando o herói atualizado
      this.dialogRef.close(updatedHero);
    } else {
      // Alerta se o formulário não for válido
      alert("Preencha todos os campos corretamente.");
    }

  }


}
