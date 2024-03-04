import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { User } from '../models/user';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  userDetails: User = {
    id: '',
    name: '',
    userEmail: '',
    city: '',
    password: '',
    
  }

  constructor(private route: ActivatedRoute, private userService: AuthService,  private routerlink: Router) { }

  ngOnInit(): void {



    
    this.route.paramMap.subscribe(
      {
        next: (params) =>{
         const id = params.get('id')

          if(id)
          {
            this.userService.getUserById(id)
            .subscribe(
              {
                next: (response) => 
                {
                     this.userDetails = response;
                },
               
              }
            )
          }
        }
      }
    )
  }


  updateUser()
  {

    this.userService.updateUser(this.userDetails.id,this.userDetails).subscribe({
      next: (response) => {
        
        this.routerlink.navigate(['/dashboard'])
      },
      error:(err) =>
      {
        alert(err?.error.message)
      }
    });
  }


  deleteUser(id:string)
  {
    this.userService.deleteUser(this.userDetails.id).subscribe({
      next: (response) => {
        this.routerlink.navigate(['/dashboard'])
      }
    });
  }

}
