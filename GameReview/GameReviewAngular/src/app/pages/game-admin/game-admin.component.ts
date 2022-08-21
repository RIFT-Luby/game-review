import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ApiPaginationResponse } from 'src/app/shared/classes/api-pagination-response/api-pagination-response';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { Game } from 'src/app/shared/entities/game.entity';
import { GameService } from 'src/app/shared/services/game.service';



@Component({
  selector: 'app-game-admin',
  templateUrl: './game-admin.component.html',
  styleUrls: ['./game-admin.component.scss']
})
export class GameAdminComponent implements OnInit {

  public columns: string[] = ["Id","Summary", "Developer", "GameGender", "Score"];
  data!: ApiPaginationResponse<Game>;
  totalPages!:number


  constructor(
    private gameService: GameService
  ) { }

  async ngOnInit() {
    await this.LoadData();
  }

  async LoadData(queryParams= new BaseParams() ){
    this.data = await lastValueFrom(this.gameService.getAllParams())
    this.totalPages = this.data.totalPages
  }
  
  onDelete(id:number){
    alert(`Deletar game de id ${id}`)
  }

}
