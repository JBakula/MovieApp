import { Component, OnInit } from '@angular/core';
import { Login } from '../interfaces/login';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  user:Login;
  formData:FormGroup;
  loggedIn:boolean;
  credentialsMessage:string = "";
  constructor(private http:UserService,private router:Router){
    this.user = {} as Login,
    this.formData = {} as FormGroup,
    this.loggedIn = {} as boolean
  }
  onSubmit(){
    if(this.formData.valid){
      
      this.user.email = this.formData.value.Email;
      this.user.password = this.formData.value.Password;

      this.http.loginUser(this.user).subscribe({
        next:(res)=>{
          
          console.log(res.body.token );
          console.log(res);
          this.formData.reset();
          this.http.storeToken(res.body.jwtToken);
          this.http.storeRefreshToken(res.body.refreshToken);
          this.http.setStatusEmitter(true);
          // this.http.getUserRatings().subscribe((res)=>{
          //   this.http.setRatingsEmitter(res);
            
          // })
          this.router.navigate(["/"]);
        },
        error:(err)=>{
          if(err.status === 400){
            this.credentialsMessage = "Wrong email or password";
          }
        }
      })
    }
  }

  ngOnInit(): void {
    this.formData = new FormGroup({
      Email: new FormControl("",[Validators.required,Validators.email]),
      Password: new FormControl("",[Validators.required,Validators.minLength(6)]),
    })
  }
}
