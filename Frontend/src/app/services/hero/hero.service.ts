import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Heroi } from 'src/app/Interfaces/Heroi.interface';
import { AppConfig } from 'src/app/config/app-config';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  private apiUrl = AppConfig.apiUrl;

  constructor(private http: HttpClient) { }

  addHero(hero: any): Observable<any> {
    return this.http.post<Heroi>(`${this.apiUrl}/heroes`, hero);
  }

  updateHero(hero: any): Observable<any> {
    return this.http.put<Heroi>(`${this.apiUrl}/heroes/${hero.id}`, hero);
  }

  deleteHero(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/heroes/${id}`);
  }

  getHeroById(id: number): Observable<any> {
    return this.http.get<Heroi>(`${this.apiUrl}/heroes/${id}`);
  }
}
