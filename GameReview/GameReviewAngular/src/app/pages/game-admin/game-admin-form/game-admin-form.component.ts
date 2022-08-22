import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Game } from 'src/app/shared/entities/game.entity';
import { GameService } from 'src/app/shared/services/game.service';

@Component({
  selector: 'app-game-admin-form',
  templateUrl: './game-admin-form.component.html',
  styleUrls: ['./game-admin-form.component.scss']
})
export class GameAdminFormComponent implements OnInit {

  form! : FormGroup
  id!: number
  game!: Game
  title!: string

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private formBuilder: FormBuilder,
  ) { 

  }

  async ngOnInit():Promise<void> {
    this.CreateForm(),
    await this.LoadData()
  }

  async LoadData(): Promise<void>{
    this.id = this.route.snapshot.params['id'] 
    if(this.route.snapshot.params['id']){
      //Edit Mode
      this.gameService.getById(this.id).subscribe(result => {
        this.game = result as Game;
        this.title = "Edit - " + this.game.name;

        //Update the form with the review value
        this.form.patchValue(this.game);
      })
    }else{
      // New Mode
      this.title = "Create new Game";
    }
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
  
}
