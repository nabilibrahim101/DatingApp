import { Component, OnInit } from '@angular/core'; // we are importing Component
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value', // click (ALT + O) to go to html page of that component. To go back is (ALT + U). (Ctrl + P) to open the GoTo File
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.scss']
})
export class ValueComponent implements OnInit {
values: any;

  constructor(private http: HttpClient) { }

  // This one is occurs after the component is initialized. and it happens after the constructor
  ngOnInit() {
    this.getValues();
  }

  // we want to call this method (getValues) when our components loads. The constructor is considered too early to get data from API,
  // so we use (ngOnInit)
  getValues()  {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

}
