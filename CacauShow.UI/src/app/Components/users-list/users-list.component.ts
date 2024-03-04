import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {

  users: User[] = [];
  constructor(private userService: AuthService) { }

  ngOnInit(): void {

    this.userService.getAllUsers().subscribe({
      next: (users) =>{
          this.users = users;
          console.log(users);
      },
      error: (response) =>
      {
        console.log(response);
      }
    })
    {

    }
      
  }

}
