import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { HeroService } from 'src/app/services/hero/hero.service';
import { HeroDetailsModalComponent } from '../modals/hero-details-modal/hero-details-modal.component';
import { MessageModalComponent } from '../modals/message-modal/message-modal.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
constructor(public dialog: MatDialog, private heroService: HeroService){}

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
