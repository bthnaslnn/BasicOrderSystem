import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/Alertify.service';
import { LookUpService } from 'app/core/services/LookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Product } from './models/product';
import { ProductService } from './services/product.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-product',
	templateUrl: './product.component.html',
	styleUrls: ['./product.component.scss']
})
export class ProductComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['productName', 'colorId', 'size', 'update', 'delete'];

	productList: Product[];
	product: Product = new Product();

	productAddForm: FormGroup;


	productId: number;

	constructor(private productService: ProductService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getProductList();
	}

	ngOnInit() {

		this.createProductAddForm();
	}


	getProductList() {
		this.productService.getProductList().subscribe(data => {
			this.productList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	save() {

		if (this.productAddForm.valid) {
			this.product = Object.assign({}, this.productAddForm.value)
			this.product.lastUpdatedUserId = this.authService.getCurrentUserId();

			if (this.product.id == 0) {
				this.product.createdUserId = this.authService.getCurrentUserId();
				console.log(this.product.createdUserId);

				this.addProduct();
			}
			else
				this.updateProduct();
		}

	}

	addProduct() {

		this.productService.addProduct(this.product).subscribe(data => {
			this.getProductList();
			this.product = new Product();
			jQuery('#product').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.productAddForm);

		})

	}

	updateProduct() {

		this.productService.updateProduct(this.product).subscribe(data => {

			var index = this.productList.findIndex(x => x.id == this.product.id);
			this.productList[index] = this.product;
			this.dataSource = new MatTableDataSource(this.productList);
			this.configDataTable();
			this.product = new Product();
			jQuery('#product').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.productAddForm);

		})

	}

	createProductAddForm() {
		this.productAddForm = this.formBuilder.group({
			id: [0],
			createdUserId: [0, Validators.required],
			lastUpdatedUserId: [0, Validators.required],
			status: [true],
			productName: ["", Validators.required],
			colorId: [0, Validators.required],
			size: ["", Validators.required]
		})
	}

	deleteProduct(productId: number) {
		this.productService.deleteProduct(productId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.productList = this.productList.filter(x => x.id != productId);
			this.dataSource = new MatTableDataSource(this.productList);
			this.configDataTable();
		})
	}

	getProductById(productId: number) {
		this.clearFormGroup(this.productAddForm);
		this.productService.getProductById(productId).subscribe(data => {
			this.product = data;
			this.productAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
			else if (key == "createdUserId")
				group.get(key).setValue(0);
			else if (key == "lastUpdatedUserId")
				group.get(key).setValue(0);
			else if (key == "status")
				group.get(key).setValue(true);
			else if (key == "productName")
				group.get(key).setValue("");
			else if (key == "colorId")
				group.get(key).setValue(0);
			else if (key == "size")
				group.get(key).setValue("");

		});
	}

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

}
