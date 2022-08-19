import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ApiBaseError } from '../../classes/api/api-base-error';
import { User } from '../../entities/user';
import { UserAdminService } from '../../services/user-admin.service';
import { UserService } from '../../services/user.service';
import { apiErrorHandler } from '../../utils/api-error-handler';

@Component({
  selector: 'app-user-password-form',
  templateUrl: './user-password-form.component.html',
  styleUrls: ['./user-password-form.component.scss']
})
export class UserPasswordFormComponent implements OnInit {

  form!: FormGroup;
  id!: any;
  isAdmin = false;

  constructor(
    private formBuilder: FormBuilder,
    private userAdminService: UserAdminService,
    private userService: UserService,
    private router: Router,
    protected http: HttpClient,
    private snackBar: MatSnackBar,
    private activatedroute:ActivatedRoute
  ) {
    this.form = this.formBuilder.group({
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]],
    })
  }

  async IsAdminAsync(): Promise<void> {
    const data = await lastValueFrom(this.userAdminService.getById(this.id));
    if(data.userRoleId == 1) {
      this.isAdmin = true;
    }else {
      this.isAdmin = false;
    }
  }

  async ngOnInit(): Promise<void> {
    this.activatedroute.paramMap.subscribe(params => {
      this.id = params.get('id');
    });
  }

  async savePasswordAsync(): Promise<void> {
    try {
      if(this.isFormValid()) {
        const data = this.form.value as User;
        if(this.isAdmin) {
          await lastValueFrom(this.userAdminService.updatePassword(data, this.id));
          this.snackBar.open('Password saved!', undefined, { duration: 3000 });
          this.router.navigate(['/home/admin/users/form/', this.id]);
        }else {
          await lastValueFrom(this.userService.updatePassword(data, this.id));
          this.snackBar.open('Password saved!', undefined, { duration: 3000 });
          this.router.navigate(['/home/user/form/', this.id]);
        }
      }
    }
    catch({error}) {
      apiErrorHandler(this.snackBar, error as ApiBaseError);
    }
  }

  isFormValid(): boolean {
    const isValid = this.form.valid;
    if (!isValid) {
      this.form.markAllAsTouched();
      this.snackBar.open('There are invalid fields in the form!', undefined, { duration: 3000 });
    }
    return isValid;
  }

}
