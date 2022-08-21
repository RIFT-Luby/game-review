import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Game } from 'src/app/shared/entities/game.entity';
import { ApiBaseService } from './api-base.service';


@Injectable({
  providedIn: 'root'
})
export class GameService extends ApiBaseService<Game>  {

  constructor(protected override http: HttpClient) {
    super("Game",http)
   }
}
