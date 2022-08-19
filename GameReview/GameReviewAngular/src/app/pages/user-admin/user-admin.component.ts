import { PageEvent } from '@angular/material/paginator';
import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom,} from 'rxjs';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { ConfirmModalService } from 'src/app/shared/components/confirm-modal/services/confirm-modal.service';
import { User } from 'src/app/shared/entities/user';
import { UserAdminService } from 'src/app/shared/services/user-admin.service';
import { ApiPaginationResponse } from 'src/app/shared/classes/api-pagination-response/api-pagination-response';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserAdminComponent implements OnInit{

  data: ApiPaginationResponse<User> = new ApiPaginationResponse<User>;
  totalPages!: number;

  constructor(
    private userAdminService: UserAdminService,
    private changeRef: ChangeDetectorRef,
    private router: Router,
    private confirmModalService: ConfirmModalService
  ) {
  }

  async ngOnInit(): Promise<void> {
    await this.getDataAsync();
  }

  async getDataAsync(params = new BaseParams()): Promise<void> {
    this.data = await lastValueFrom(this.userAdminService.getAllParams(params));
    this.totalPages = this.data.totalPages;
  }

  async refresh(): Promise<void>{
    await this.getDataAsync();
    this.changeRef.detectChanges();
  }

  onAdd(): void {
    this.router.navigate(['/home/admin/users/form/0']);
  }

  onEdit(id: number): void {
    this.router.navigate(['/home/admin/users/form/', id]);
  }

  async deleteUserAsync(id: number): Promise<void> {
    this.confirmModalService.open();
    this.confirmModalService.closed.subscribe(async (result) => {
      if(result) {
        await lastValueFrom(this.userAdminService.delete(id));
        await this.refresh();
      }
    });
  }

  async changePage(event: PageEvent): Promise<void>{
    const params = {
      take: event.pageSize,
      skip: event.pageIndex * event.pageSize,
    } as BaseParams;
    await this.getDataAsync(params);
  }
}



