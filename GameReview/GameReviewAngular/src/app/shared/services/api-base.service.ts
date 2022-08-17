import { environment } from './../../../environments/environment';
import { BaseParams } from './../classes/params/base-params';
import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { ApiPaginationResponse } from '../classes/api-pagination-response/api-pagination-response';

@Injectable({
  providedIn: 'root'
})
export class ApiBaseService<T> {

  env: string = environment.API;

  constructor(
    @Inject('root') protected route: string,
    protected http: HttpClient
  ) { }

  getAllParams(params = new BaseParams()): Observable<ApiPaginationResponse<T>>{
    return this.http.get<ApiPaginationResponse<T>>(`${this.env}${this.route}`, {params});
  }

  getById(id: number){
    return this.http.get<T>(`${this.env}${this.route}/${id}`).pipe(take(1));
  }

  create(register: T){
    return this.http.post<T>(`${this.env}${this.route}`, register).pipe(take(1));
  }

  update(register: T, id: number){
    return this.http.put<T>(`${this.env}${this.route}/${id}`, register).pipe(take(1));
  }

  delete(id: number): Observable<void>{
    return this.http.delete<void>(`${this.env}${this.route}/${id}`).pipe(take(1));
  }
}
