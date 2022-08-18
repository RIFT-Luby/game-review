import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom, Observable } from 'rxjs';
import { ApiPaginationResponse } from 'src/app/shared/classes/api-pagination-response/api-pagination-response';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
import { ConfirmModalService } from 'src/app/shared/components/confirm-modal/services/confirm-modal.service';
import { User } from 'src/app/shared/entities/user';
import { UserAdminService } from 'src/app/shared/services/user-admin.service';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.scss']
})
export class UserAdminComponent implements OnInit{

  data: ApiPaginationResponse<User> = new ApiPaginationResponse<User>;

  constructor(
    private userAdminService: UserAdminService,
    private router: Router,
    private confirmModalService: ConfirmModalService
  ) {
  }

  async ngOnInit(): Promise<void> {
    await this.getDataAsync();
  }

  async getDataAsync(params = new BaseParams()): Promise<void> {
    this.data = await lastValueFrom(this.userAdminService.getAllParams(params));
  }

  async refreshTableAsync(): Promise<void> {
    await this.getDataAsync();
    //this.cdRef.detectChanges();
  }

  onAdd() {
    this.router.navigate(['/home/admin/users/form/0']);
  }

  onEdit(id: number) {
    this.router.navigate(['/home/admin/users/form/', id]);
  }

  async deleteUserAsync(id: number): Promise<void> {
    this.confirmModalService.open();
    this.confirmModalService.closed.subscribe(async (result) => {
      if(result) {
        await lastValueFrom(this.userAdminService.delete(id));
        await this.refreshTableAsync();
      }
    });
  }
}
