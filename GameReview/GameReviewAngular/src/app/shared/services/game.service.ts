import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Game } from 'src/app/shared/entities/game.entity';
import { Enumeration } from '../entities/enumeration';
import { ApiBaseService } from './api-base.service';


@Injectable({
  providedIn: 'root'
})
export class GameService extends ApiBaseService<Game>  {

  constructor(protected override http: HttpClient) {
    super("Game",http)
   }

  async GetGameTypesAsync():Promise<Enumeration[]>{
    let response = this.http.get<Enumeration[]>(`${this.env}Game/game-genders`); 
    return lastValueFrom(response);
  }

}
