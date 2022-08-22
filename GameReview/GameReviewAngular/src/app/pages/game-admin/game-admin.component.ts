import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { lastValueFrom } from 'rxjs';
import { ApiPaginationResponse } from 'src/app/shared/classes/api-pagination-response/api-pagination-response';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { ConfirmModalService } from 'src/app/shared/components/confirm-modal/services/confirm-modal.service';
import { Game } from 'src/app/shared/entities/game.entity';
import { GameService } from 'src/app/shared/services/game.service';



@Component({
  selector: 'app-game-admin',
  templateUrl: './game-admin.component.html',
  styleUrls: ['./game-admin.component.scss']
})
export class GameAdminComponent implements OnInit {

  public columns: string[] = ["id" , "name" , "summary" , "developer", "gameGender", "score"];
  data!: ApiPaginationResponse<Game>;
  totalPages!:number


  constructor(
    private gameService: GameService,
    private confirmModal: ConfirmModalService,
    private changeRef: ChangeDetectorRef
  ) { }

  async ngOnInit() {
    await this.LoadData();
  }

  async LoadData(queryParams= new BaseParams() ){
    this.data = await lastValueFrom(this.gameService.getAllParams())
    this.totalPages = this.data.totalPages
  }

  async Refresh():Promise<void>{
    await this.LoadData();
    this.changeRef.detectChanges();
  }

  async onDelete(id:number):Promise<void>{
    this.confirmModal.open();
    this.confirmModal.closed.subscribe(async (result) => {
      if (result) {
        await lastValueFrom(this.gameService.delete(id));
        await this.Refresh();
      }
    });
  }

  async changePage(event: PageEvent): Promise<void>{
    const params = {
      take: event.pageSize,
      skip: event.pageIndex * event.pageSize,
    } as BaseParams;
    await this.LoadData(params);
  }

}
