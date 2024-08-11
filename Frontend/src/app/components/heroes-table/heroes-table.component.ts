import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Heroi } from 'src/app/Interfaces/Heroi.interface';
import { HeroService } from 'src/app/services/hero/hero.service';
import { MessageModalComponent } from '../modals/message-modal/message-modal.component';
import { RemoveModalComponent } from '../modals/remove-modal/remove-modal.component';
import { UpdateHeroModalComponent } from '../modals/update-hero-modal/update-hero-modal.component';
import { SuperpowerService } from 'src/app/services/superPower/superpower.service';
import { Superpoder } from 'src/app/Interfaces/Superpoder.interface';
import { HeroDetailsModalComponent } from '../modals/hero-details-modal/hero-details-modal.component';
import { InteractionService } from 'src/app/services/interaction/interaction.service';

@Component({
  selector: 'app-heroes-table',
  templateUrl: './heroes-table.component.html',
  styleUrls: ['./heroes-table.component.scss']
})
export class HeroesTableComponent implements OnInit {
  dataSource: MatTableDataSource<Heroi> = new MatTableDataSource()
  superpowers: Superpoder[] = [];
  displayColumns: string[] = ["id", "nome", "nomeHeroi", "dataNascimento", "altura", "peso", "superpoderes", "acoes"]
  heroId: string = '';
  isLoading: boolean = true;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private heroService: HeroService, public dialog: MatDialog, private superpowerService: SuperpowerService,
    private interactionService: InteractionService
  ) { }

  ngOnInit(): void {

    this.interactionService.heroes.subscribe(data => {
      this.dataSource.data = data
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.isLoading = false;
    })
    this.superpowerService.getSuperpowers().subscribe((data: any) => {
      this.superpowers = data;
    })
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  editHero(hero: Heroi) {
    const dialogRef = this.dialog.open(UpdateHeroModalComponent, {
      data: { hero: hero, superpowers: this.superpowers }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.heroService.updateHero(result).subscribe({
          next: (updatedHero) => {
            const index = this.dataSource.data.findIndex(h => h.id === updatedHero.Id);
            if (index !== -1) {
              this.dataSource.data[index] = updatedHero;
              this.dataSource._updateChangeSubscription();
            }
            this.dialog.open(MessageModalComponent, {
              data: { message: "Herói atualizado com sucesso!" }
            });
          },
          error: (error) => {
            this.dialog.open(MessageModalComponent, {
              data: { message: error.error ? error.error.message : 'Erro ao atualizar herói' }
            });
          }
        });
      }
    });
  }


  deleteHero(hero: Heroi) {
    const dialogRef = this.dialog.open(RemoveModalComponent, {
      data: { name: "Herói" }
    });

    dialogRef.afterClosed().subscribe({
      next: (result) => {
        if (result) {
          this.heroService.deleteHero(hero.id).subscribe({
            next: (data) => {
              this.dialog.open(MessageModalComponent, {
                data: { message: data.message }
              });
            },
            error: (error) => {
              this.dialog.open(MessageModalComponent, {
                data: { message: error.error.message }
              });
            }
          });
        }
      },
      error: () => {
        this.dialog.open(MessageModalComponent, {
          data: { message: "Erro interno na interface de usuário." }
        });
      }
    });
  }

  searchHeroById(heroId: string): void {
    this.heroService.getHeroById(parseInt(heroId)).subscribe(hero => {
      this.dialog.open(HeroDetailsModalComponent, {
        data: { hero: hero }
      });
    }, error => {
      console.error('Error fetching hero', error);
      this.dialog.open(MessageModalComponent, {
        data: { message: 'Herói não encontrado.' }
      });
    });
  }
}