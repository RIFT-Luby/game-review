import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../entities/user';
import { ApiBaseService } from './api-base.service';

@Injectable({
  providedIn: 'root'
})
export class UserAdminService extends ApiBaseService<User> {

  constructor(
    protected override http: HttpClient,
  ) {
    super("UserAdmin", http)
   }

}
