import { Enumeration } from "./enumeration";
import { Register } from "./register";

export class User extends Register{
  name!: string;
  email!: string;
  userName!: string;
  userRole!: Enumeration;
  userRoleId?: number;
  password?: string;
  confirmPasswood?: string;
  image!: {url: string};
}
