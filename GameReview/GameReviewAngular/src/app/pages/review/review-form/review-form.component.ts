import { take, lastValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Review } from 'src/app/shared/entities/review.entity';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { ReviewService } from 'src/app/shared/services/review.service';
import { apiErrorHandler } from 'src/app/shared/utils/api-error-handler';
import { ApiBaseError } from 'src/app/shared/classes/api/api-base-error';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';

@Component({
  selector: 'app-review-form',
  templateUrl: './review-form.component.html',
  styleUrls: ['./review-form.component.scss']
})
export class ReviewFormComponent implements OnInit {

   title!: string;
   form!: FormGroup;
   review!: Review;
   id?:number;

  constructor(
    private reviewService: ReviewService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    private _ngZone: NgZone){}

    async ngOnInit(): Promise<void> {
      this.createForm();
      await this.loadData();

    }

    async loadData(): Promise<void>{
      this.id = this.route.snapshot.params['id'];

      if(this.id){
        //Edit Mode
        this.reviewService.getById(this.id).subscribe(result => {
          this.review = result as Review;
          this.title = "Edit - " + this.review.gameId;

          //Update the form with the review value
          this.form.patchValue(this.review);
        })
      }else{
        // New Mode
        this.title = "Create new Review";
      }
    }

    async createAsync(): Promise<void>{

      const review = (this.id) ? this.review : <Review>{};

      review.gameId = this.form.get("gameId")?.value;
      review.userReview = this.form.get("userReview")?.value;
      review.score = this.form.get("score")?.value;

      try{
        if(this.isFormValid()){
          if(this.id){
            //Edit Mode
            await lastValueFrom(this.reviewService.update(review, this.id));
            this.router.navigate(['/home/reviews']);
          }else{
            //Add Mode
            const data = this.form.value as Review;
            await lastValueFrom(this.reviewService.create(data));
            this.router.navigate(['/home/reviews']);
          }
        }
      }
      catch({error}){
        apiErrorHandler(this.snackBar, error as ApiBaseError);
      }
    }

    createForm(){
      this.form = this.formBuilder.group
        ({
            gameId: [null, [Validators.required]],
            userReview: [null, [Validators.required]],
            score: [null, [Validators.required]],
        });
    }

    isFormValid(): boolean{
      const isValid = this.form.valid;
      if(!isValid)
      {
        this.form.markAllAsTouched();
        this.snackBar.open("Campos inválidos no formulário!", undefined, { duration: 5000});
      }

      return isValid;
    }

  @ViewChild('autosize') autosize!: CdkTextareaAutosize;

  triggerResize() {
    // Wait for changes to be applied, then trigger textarea resize.
    this._ngZone.onStable.pipe(take(1)).subscribe(() => this.autosize.resizeToFitContent(true));
  }

}
