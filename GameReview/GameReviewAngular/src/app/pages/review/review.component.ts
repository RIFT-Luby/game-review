import { ConfirmModalService } from './../../shared/components/confirm-modal/services/confirm-modal.service';
import { lastValueFrom } from 'rxjs';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Review } from 'src/app/shared/entities/review.entity';
import { ApiPaginationResponse } from 'src/app/shared/classes/api/api-pagination-response';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { ReviewService } from 'src/app/shared/services/review.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReviewComponent implements OnInit {

  public columns: string[] = ["id","gameName", "userReview", "score", "edit", "delete"];
  data!: ApiPaginationResponse<Review>;
  totalPages!: number;

  constructor(
    private reviewService: ReviewService,
    private confirmModal: ConfirmModalService,
    private changeRef: ChangeDetectorRef) { }

  async loadData(params = new BaseParams()): Promise<void>{
    this.data = await lastValueFrom(this.reviewService.getAllParams(params));
    this.totalPages = this.data.totalPages;
  }


  async ngOnInit(): Promise<void> {
    await this.loadData();
  }

  async refresh(): Promise<void>{
    await this.loadData();
    this.changeRef.detectChanges();
  }

  async onDelete(id: number): Promise<void>{
    this.confirmModal.open();
    this.confirmModal.closed.subscribe(async (result) => {
      if (result) {
        await lastValueFrom(this.reviewService.delete(id));
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
