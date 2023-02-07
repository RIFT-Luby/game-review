import { User } from 'src/app/shared/entities/user';
import { Game } from './game.entity';

export class Review{
  id!: number;
  userId!: number;
  gameId!: number;
  userReview!: string;
  score!: number;
  user!: User;
  game!: Game;
}
