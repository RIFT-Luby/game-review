import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { take, lastValueFrom } from 'rxjs';
import { Enumeration } from 'src/app/shared/entities/enumeration';
import { Game } from 'src/app/shared/entities/game.entity';
import { GameService } from 'src/app/shared/services/game.service';

@Component({
  selector: 'app-game-admin-form',
  templateUrl: './game-admin-form.component.html',
  styleUrls: ['./game-admin-form.component.scss']
})
export class GameAdminFormComponent implements OnInit {

  form! : FormGroup
  id?: number
  game!: Game
  title!: string
  gameTypeList!:Enumeration[]

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router,
    private _ngZone: NgZone
  ) { 

  }

  async ngOnInit():Promise<void> {
    this.CreateForm(),
    await this.LoadData()
  }

  async LoadData(): Promise<void>{
    this.id = this.route.snapshot.params['id']
     
    if(this.id){
      //Edit Mode
      this.gameService.getById(this.id).subscribe(result => {
        this.game = result as Game;
        this.title = "Edit - " + this.game.name;

        this.form.patchValue(this.game);
      })
    }else{
      // New Mode
      this.title = "Create new Game";
    }
    this.gameTypeList = await this.gameService.GetGameTypesAsync();
  }

  CreateForm(){
    this.form = this.formBuilder.group
      ({
          name: [null, [Validators.required]],
          summary: [null, [Validators.required]],
          developer: [null, [Validators.required]],
          gameGenderId: [null, [Validators.required]],
          score: [null, [Validators.required]],
          console: [null, [Validators.required]]
      });
  }
  
  async CreateAsync(): Promise<void>{
    const game = (this.id) ? this.game : <Game>{};
    game.name = this.form.get("name")?.value;
    game.summary = this.form.get("summary")?.value;
    game.developer = this.form.get("developer")?.value;
    game.gameGenderId= this.form.get("gameGenderId")?.value;
    game.score= this.form.get("score")?.value;
    game.console= this.form.get("console")?.value

    try{
      if(this.isFormValid()){
        if(this.id){
          //Edit Mode
          await lastValueFrom(this.gameService.update(game, this.id));
          this.router.navigate(['/home/admin/games']);
        }else{
          //Add Mode
          const data = this.form.value as Game;
          await lastValueFrom(this.gameService.create(data));
          this.router.navigate(['/home/admin/games']);
        }
      }
    }
    catch({error}){
      //apiErrorHandler(this.snackBar, error as ApiBaseError);
    }
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
