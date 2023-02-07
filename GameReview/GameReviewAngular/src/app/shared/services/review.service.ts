import { HttpClient } from '@angular/common/http';
import { ApiBaseService } from './api-base.service';
import { Injectable } from '@angular/core';
import { Review } from '../entities/review.entity';

@Injectable({
  providedIn: 'root'
})
export class ReviewService extends ApiBaseService<Review> {

  constructor(protected override http: HttpClient)
  {
    super("Review", http);
  }
}
