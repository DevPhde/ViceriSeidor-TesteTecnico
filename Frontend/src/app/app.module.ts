import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeroesTableComponent } from './components/heroes-table/heroes-table.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MessageModalComponent } from './components/modals/message-modal/message-modal.component';
import { MatDialogModule } from '@angular/material/dialog';
import { RemoveModalComponent } from './components/modals/remove-modal/remove-modal.component';
import { UpdateHeroModalComponent } from './components/modals/update-hero-modal/update-hero-modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CreateHeroModalComponent } from './components/modals/create-hero-modal/create-hero-modal.component';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { HeroDetailsModalComponent } from './components/modals/hero-details-modal/hero-details-modal.component';
import { MatChipsModule } from '@angular/material/chips';
import { HomeComponent } from './pages/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';

@NgModule({
  declarations: [
    AppComponent,
    HeroesTableComponent,
    MessageModalComponent,
    RemoveModalComponent,
    UpdateHeroModalComponent,
    CreateHeroModalComponent,
    HeroDetailsModalComponent,
    HomeComponent,
    NavbarComponent,
  ],
  imports: [
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatChipsModule,
    MatTooltipModule,
    MatIconModule,
    FormsModule,
    MatCheckboxModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    BrowserAnimationsModule,
    MatDialogModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
