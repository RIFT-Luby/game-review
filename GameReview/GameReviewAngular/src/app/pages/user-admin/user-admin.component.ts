import { ConfirmModalService } from './../../shared/components/confirm-modal/services/confirm-modal.service';
import { PageEvent } from '@angular/material/paginator';
import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom,} from 'rxjs';
import { ApiPaginationResponse } from 'src/app/shared/classes/api/api-pagination-response';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { User } from 'src/app/shared/entities/user';
import { UserAdminService } from 'src/app/shared/services/user-admin.service';

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
    private confirmModal: ConfirmModalService,
    private changeRef: ChangeDetectorRef,
    private router: Router,
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
    this.confirmModal.open();
    this.confirmModal.closed.subscribe(async (result) => {
      if (result) {
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



