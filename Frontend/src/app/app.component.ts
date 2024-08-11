import { Component, OnInit } from '@angular/core';
import { CreateHeroModalComponent } from './components/modals/create-hero-modal/create-hero-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { HeroService } from './services/hero/hero.service';
import { MessageModalComponent } from './components/modals/message-modal/message-modal.component';
import { SignalRService } from './services/signalR/signalR.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'superheroes';

  constructor(public dialog: MatDialog, private heroService: HeroService, public signalRService: SignalRService) { }
  ngOnInit(): void {
    this.signalRService.startConnection();
  }

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
