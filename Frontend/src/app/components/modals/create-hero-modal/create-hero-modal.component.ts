import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SuperpowerService } from 'src/app/services/superPower/superpower.service';
import { MessageModalComponent } from '../message-modal/message-modal.component';

@Component({
  selector: 'app-create-hero-modal',
  templateUrl: './create-hero-modal.component.html',
  styleUrls: ['./create-hero-modal.component.scss']
})
export class CreateHeroModalComponent {
  heroForm: FormGroup;
  superpowers: any[] = [];
  constructor(
    public dialogRef: MatDialogRef<CreateHeroModalComponent>,
    private fb: FormBuilder,
    private superpowerService: SuperpowerService,
    public dialog: MatDialog
  ) {
    this.heroForm = this.fb.group({
      nome: ['', Validators.required],
      nomeHeroi: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      altura: ['', Validators.required],
      peso: ['', Validators.required],
      superpoderes: this.fb.array([])
    });

    this.superpowerService.getSuperpowers().subscribe(data => {
      this.superpowers = data;
      this.addSuperpowersCheckboxes();
    });
  }

  private addSuperpowersCheckboxes() {
    this.superpowers.forEach(() => this.superpoderesFormArray.push(this.fb.control(false)));
  }

  get superpoderesFormArray() {
    return this.heroForm.controls['superpoderes'] as FormArray;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSaveClick(): void {
    if (this.heroForm.valid) {
      const selectedSuperpowers = this.heroForm.value.superpoderes
        .map((checked: boolean, index: number) => checked ? this.superpowers[index] : null)
        .filter((superpower: any) => superpower !== null);

      if (selectedSuperpowers.length === 0) {
        alert("Selecione pelo menos um superpoder.");
        this.dialog.open(MessageModalComponent, {
          data: { message: "Selecione pelo menos um superpoder." }
        })
        return;
      }

      const newHero = {
        ...this.heroForm.value,
        superpoderes: selectedSuperpowers
      };

      this.dialogRef.close(newHero);
    } else {
      this.dialog.open(MessageModalComponent, {
        data: { message: "Preencha todos os campos corretamente." }
      })
      return;
    }
  }
}
