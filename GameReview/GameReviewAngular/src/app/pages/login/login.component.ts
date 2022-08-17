import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { take, lastValueFrom } from 'rxjs';
import { apiErrorHandler } from 'src/app/shared/utils/api-error-handler';
import { AuthUser } from 'src/app/shared/entities/auth-user.entity';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ApiBaseError } from 'src/app/shared/classes/api/api-base-error';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form!: FormGroup;
  authUser!: AuthUser;
  error!: Error;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router)
    {
      this.form = this.formBuilder.group({
        username: [null, [Validators.required]],
        password: [null, [Validators.required]]
      })
    }

  ngOnInit(): void {
  }

  async loginAsync(): Promise<void>{
    try {
      if(this.isFormValid()){
        const data = this.form.value as AuthUser;
        const  { token } = await lastValueFrom (this.authService.loginUser(data));
        this.authService.setToken(token);
        this.router.navigate(['/home'])

      }
    }
    catch({error}){
      apiErrorHandler(this.snackBar, error as ApiBaseError);
    }
  }

  isFormValid(): boolean{
    const isValid = this.form.valid;
    if(!isValid)
    {
      this.form.markAllAsTouched();
      this.snackBar.open("Invalid fields on form!", undefined, { duration: 5000});
    }

    return isValid;

  }

}
