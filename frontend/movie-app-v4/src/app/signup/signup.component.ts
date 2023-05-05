import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Registration } from '../interfaces/registration';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit{
  formData:FormGroup
  newUser:Registration
  password:string = "";
  constructor(private http:UserService,private router:Router){
    this.formData = {} as FormGroup
    this.newUser = {} as Registration
  }
  handleKeyUp(event:any){
    this.password = event.target.value;
    console.log(this.password);
  }

  onSubmit(){
    if(this.formData.valid){
      this.newUser.name = this.formData.value.Name;
      this.newUser.lastname = this.formData.value.Lastname;
      this.newUser.email = this.formData.value.Email;
      this.newUser.password = this.formData.value.Password;

      this.http.registerUser(this.newUser).subscribe({
        next:(res)=>{
          if(res.status === 200){
            this.router.navigate(["/login"]);
          }
        },
        error:(err)=>{
          console.log(err.status)
        }
      })
    }
  }
  // passwordConfirm(control:FormControl){
  //   if(control.value != this.password){
  //     return {passwordConfirm:true}
  //   }else{
  //     return null;
  //   }
  // }
  ngOnInit():void{
   this.formData = new FormGroup({
      Name:new FormControl("",[Validators.required]),
      Lastname: new FormControl("",[Validators.required]),
      Email: new FormControl("",[Validators.required,Validators.email]),
      Password: new FormControl("",[Validators.required,Validators.minLength(6)]),
      ConfirmPassword: new FormControl("",[Validators.required])
    })
  }
}
