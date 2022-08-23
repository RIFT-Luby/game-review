import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
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

  public columns: string[] = ["id" , "name" , "summary" , "developer", "gameGender", "console", "score","edit", "delete"];
  data: ApiPaginationResponse<Game> = new ApiPaginationResponse<Game>();
  params = ["Name", "Developer", "ScoreMaiorQue", "ScoreMenorQue", "Console"];
  search!: FormGroup;
  totalPages!: number;
  value!: string;

  constructor(
    private gameService: GameService,
    private confirmModal: ConfirmModalService,
    private changeRef: ChangeDetectorRef
  ) { }

  ngOnInit(): void{
    this.loadData();
  }

  async refresh(): Promise<void>{
    await this.loadData();
    this.changeRef.detectChanges();
  }

  async loadData(params = new BaseParams()): Promise<void>{
    this.data = await lastValueFrom(this.gameService.getAllParams(params));
    this.totalPages = this.data.totalPages;
  }

  async loadParam(field: string, target: any): Promise<void> {
    if(target instanceof EventTarget) {
      let elemento = target as HTMLInputElement;
      this.value = elemento.value as string;
    }
    const params = {
      [field]: this.value
    } as BaseParams;
    await this.loadData(params);
  }

  async onDelete(id:number):Promise<void>{
    this.confirmModal.open();
    this.confirmModal.closed.subscribe(async (result) => {
      if (result) {
        await lastValueFrom(this.gameService.delete(id));
        await this.refresh();
      }
    });
  }

  async changePage(event: PageEvent): Promise<void>{
    const params = {
      take: event.pageSize,
      skip: event.pageIndex * event.pageSize,
    } as BaseParams;
    await this.loadData(params);
  }

}
