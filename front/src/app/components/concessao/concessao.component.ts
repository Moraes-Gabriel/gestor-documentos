import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-concessao',
  templateUrl: './concessao.component.html',
  styleUrls: ['./concessao.component.scss']
})
export class ConcessaoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
