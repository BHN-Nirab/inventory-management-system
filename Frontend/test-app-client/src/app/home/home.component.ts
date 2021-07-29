import { Reports } from './../models/log';
import { ProductService } from './../product.service';
import { Product } from './../models/product';
import { User } from '../models/user';

import { Component, OnInit, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isOpenAddProduct: boolean = false;
  isOpenUpdateProduct: boolean = false;
  isOpenReports: boolean = false;

  user: User = new User();
  dateTime: Date = new Date();
  products: Product[] = [];
  product: Product = new Product();

  reports: Reports[] = [];

  constructor(private productService: ProductService) { }

  @Output() messageEvent = new EventEmitter<any>();

  ngOnInit() {
    this.user.Id = Number.parseInt(localStorage.getItem("Id"));
    this.user.Token = localStorage.getItem("token");
    this.user.Name = localStorage.getItem("Name");
    this.getAll();
  }

  toggleOpenAddProduct(value: boolean){
    this.isOpenUpdateProduct = false;
    this.isOpenReports = false;
    this.isOpenAddProduct = value;
  }

  toggleOpenUpdateProduct(value: boolean){
    this.isOpenAddProduct = false;
    this.isOpenReports = false;
    this.isOpenUpdateProduct = value;
  }

  toggleOpenReports(value: boolean, isDaily:boolean){
    this.isOpenAddProduct = false;
    this.isOpenUpdateProduct = false;
    this.isOpenReports = value;

    if(value)
    {
      this.getReport(isDaily);
    }
  }

  logout(){
    this.messageEvent.emit();
  }

  getAll(){
    this.productService.getAll().subscribe(response=>{

      this.products = [];

     response.products.forEach(element => {
       var p = new Product();
       p.Id = Number.parseInt(element.id);
       p.Name = element.name;
       p.Description = element.description;
       p.Unit = Number.parseInt(element.unit);
       p.UnitPrice = Number.parseFloat(element.unitPrice);
       p.UserId = Number.parseInt(element.userId);
       p.UserName = element.userName;

       this.products.push(p);
       
     });

    });

  }

  add(){
    if(this.product==null || this.product.Name==null || this.product.Description==null || this.product.UnitPrice==null || this.product.Unit==null)
      return;

    this.product.UserName = localStorage.getItem('Name');
    this.product.UserId = Number.parseInt(localStorage.getItem('Id'));

    var data =  {
      Product : this.product,
      token : localStorage.getItem('token'),
      UserId : Number.parseInt(localStorage.getItem('Id')),
      userName : localStorage.getItem('Name')
    };

    this.productService.add(data).subscribe(response=>{
      this.product = new Product();
      this.getAll();
    });

  }

  delete(productId: number){
   
    this.productService.delete(productId).subscribe(response=>{
      this.getAll();
    });

  }

  initializeUpdateProduct(product: Product){
    this.product = product;
    this.toggleOpenUpdateProduct(true);
  }

  update(){
    this.productService.update(this.product).subscribe(response=>{
      this.product = new Product();
      this.getAll();
    });

  }

  purchase(product: Product){
    this.productService.purchase(product.Id, product.SaleAndPurchaseUnit).subscribe(response=>{
      console.log(response);
      this.getAll();
    });

  }

  sale(product: Product){
    this.productService.sale(product.Id, product.SaleAndPurchaseUnit).subscribe(response=>{
      this.getAll();
    });

  }


  getReport(isDaily: boolean){
    this.productService.getReport(this.dateTime, isDaily).subscribe(response=>{

      this.reports = [];

      var allReports = response.Reports;

      console.log(response.Reports);

      allReports.forEach(element => {
        var data = new Reports();
        data.UserName = element.UserName;
        data.ProductName = element.ProductName;
        data.ProductDescription = element.ProductDescription;
        data.Unit = element.Unit;
        data.UnitPrice = element.UnitPrice;
        data.TransactionType = element.TransactionType;
        data.Created = element.Created;

        this.reports.push(data);

      });
      
    });
  }

}
