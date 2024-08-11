import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { GenerateUUID } from 'src/app/Helpers/GenerateUUID';
import { Heroi } from 'src/app/Interfaces/Heroi.interface';

@Injectable({
  providedIn: 'root'
})
export class InteractionService {

  constructor() { }

  public UUID = new BehaviorSubject<string>(GenerateUUID.Generate());
  setUUID(uuid: string): void {
    this.UUID.next(uuid);
  }

  public heroes = new BehaviorSubject<Heroi[]>([]);
  setHeroes(heroes: Heroi[]) {
    this.heroes.next(heroes);
  }

}
