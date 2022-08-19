import { ConfirmModalService } from './../../shared/components/confirm-modal/services/confirm-modal.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { lastValueFrom } from 'rxjs';
import { Review } from 'src/app/shared/entities/review.entity';
import { Component, OnInit, ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import { ApiPaginationResponse } from 'src/app/shared/classes/api/api-pagination-response';
import { ReviewAdminService } from 'src/app/shared/services/review-admin.service';
import { BaseParams } from 'src/app/shared/classes/params/base-params';

@Component({
  selector: 'app-review-admin',
  templateUrl: './review-admin.component.html',
  styleUrls: ['./review-admin.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReviewAdminComponent implements OnInit {

  public columns: string[] = ["id", "gameName", "userReview", "score", "delete", "userName"];
  data!: ApiPaginationResponse<Review>;
  search!: FormGroup;
  params = ["UserName", "GameName", "ScoreMaiorQue", "ScoreMenorQue"];
  value!: string;
  totalPages!: number;

  constructor(
    private reviewAdminService: ReviewAdminService,
    private confirmModal: ConfirmModalService,
    private formBuilder: FormBuilder,
    private changeRef: ChangeDetectorRef)
    {
      this.search = this.formBuilder.group({
        param: [null],
        value: [null]
      });
    }

  ngOnInit(): void{
    this.loadData();
  }

  async refresh(): Promise<void>{
    await this.loadData();
    this.changeRef.detectChanges();
  }

  async loadData(params = new BaseParams()): Promise<void>{
    this.data = await lastValueFrom(this.reviewAdminService.getAllParams(params));
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

  async onDelete(id: number): Promise<void>{
    this.confirmModal.open();
    this.confirmModal.closed.subscribe(async (result) => {
      if (result) {
        await lastValueFrom(this.reviewAdminService.delete(id));
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
