import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom, Observable } from 'rxjs';
import { ApiPaginationResponse } from 'src/app/shared/classes/api-pagination-response/api-pagination-response';
import { BaseParams } from 'src/app/shared/classes/params/base-params';
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
  ) {
  }

  async ngOnInit(): Promise<void> {
    await this.getDataAsync();
  }

  async getDataAsync(params = new BaseParams()): Promise<void> {
    this.data = await lastValueFrom(this.userAdminService.getAllParams(params));
  }

  onAdd() {
    this.router.navigate(['/userAdmin/form/0']);
  }

  onEdit(id: number) {
    this.router.navigate(['/userAdmin/form/', id]);
  }

  async deleteUserAsync(id: number): Promise<void> {
    //TODO confirm exclusion
    await lastValueFrom(this.userAdminService.delete(id));
  }
}
