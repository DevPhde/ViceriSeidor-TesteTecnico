import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfig } from 'src/app/config/app-config';
import { Superpoder } from 'src/app/Interfaces/Superpoder.interface';

@Injectable({
  providedIn: 'root'
})
export class SuperpowerService {
  private apiUrl = AppConfig.apiUrl;
  constructor(private http: HttpClient) { }

  getSuperpowers(): Observable<any> {
    return this.http.get<Superpoder[]>(`${this.apiUrl}/superpowers`);
  }

  addSuperpower(superpower: Superpoder): Observable<any> {
    return this.http.post<Superpoder>(`${this.apiUrl}/superpowers`, superpower);
  }

  deleteSuperpower(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/superpowers/${id}`);
  }
}
