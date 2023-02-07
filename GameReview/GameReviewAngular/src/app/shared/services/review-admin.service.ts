import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Review } from '../entities/review.entity';
import { ApiBaseService } from './api-base.service';

@Injectable({
  providedIn: 'root'
})
export class ReviewAdminService extends ApiBaseService<Review> {

  constructor(protected override http: HttpClient)
  {
    super("ReviewAdmin", http);
  }
}
