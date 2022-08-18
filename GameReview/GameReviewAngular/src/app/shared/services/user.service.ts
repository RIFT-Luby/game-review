import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../entities/user';
import { ApiBaseService } from './api-base.service';
import { take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService extends ApiBaseService<User>{

  constructor(
    protected override http: HttpClient,
  ) {
    super("User", http)
   }

   updatePassword(user: User, id: number){
    return this.http.put<User>(`${this.env}${this.route}/password`, user).pipe(take(1));
  }
}
