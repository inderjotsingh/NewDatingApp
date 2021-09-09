import { AccountService } from './../_services/account.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponent: any;   // Parent to Child
  @Output() cancelRegister = new EventEmitter();   // Child to Parent
  model: any = {};
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register() {
    console.log(this.model);
    this.accountService.register(this.model).subscribe(response => {
      console.log(response);
      this.cancel();
    }, error => {
      console.log(error);
    })
  }

  cancel() {
    console.log('cancelled');
    this.cancelRegister.emit(false);
  }

}
