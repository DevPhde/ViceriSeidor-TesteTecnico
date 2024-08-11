import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateHeroModalComponent } from 'src/app/components/modals/create-hero-modal/create-hero-modal.component';
import { MessageModalComponent } from 'src/app/components/modals/message-modal/message-modal.component';
import { HeroService } from 'src/app/services/hero/hero.service';
import { SignalRService } from 'src/app/services/signalR/signalR.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(public dialog: MatDialog, private heroService: HeroService, public signalRService: SignalRService) { }

  openCreateHeroModal(): void {
    const dialogRef = this.dialog.open(CreateHeroModalComponent);

    dialogRef.afterClosed().subscribe({
      next: (result) => {
        if (result) {
          this.heroService.addHero(result).subscribe({
            next: () => {
              this.dialog.open(MessageModalComponent, {
                data: { message: "Novo Herói cadastrado." }
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
}
