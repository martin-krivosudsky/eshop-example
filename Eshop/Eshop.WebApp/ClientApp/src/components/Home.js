import React, { Component } from 'react';
import InfiniteScroll from 'react-infinite-scroll-component'
import { ProductDetailComponent } from './ProductDetail'
import axios from 'axios';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);

        this.state = {
            items: [],
            take: 10,
            skip: 0,
            endOfList: false,
            isModalOpen: false,
            selectedProduct: null,
        };

        this.fetchData = this.fetchData.bind(this);
        this.handleRowClick = this.handleRowClick.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleSave = this.handleSave.bind(this);
        this.changeDescription = this.changeDescription.bind(this);
        this.fetchData();
    }

    render() {
        return (
            <>
                <InfiniteScroll
                    dataLength={this.state.items.length}
                    next={this.fetchData}
                    hasMore={!this.state.endOfList}
                    loader={<h4>Loading...</h4>}
                    endMessage={
                        <p style={{ textAlign: 'center' }}>
                            <b>You have seen it all</b>
                        </p>
                    }
                >
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Name</th>
                                <th>Price</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.items.map(product =>
                                <tr key={product.id} onClick={() => this.handleRowClick(product.id)}>
                                    <td><img src={product.imgUri} alt={product.name} /></td>
                                    <td>{product.name}</td>
                                    <td>{product.price}</td>
                                    <td>{product.description}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </InfiniteScroll>
                <ProductDetailComponent
                    isOpen={this.state.isModalOpen}
                    handleClose={this.handleClose}
                    handleSave={this.handleSave}
                    product={this.state.selectedProduct}
                    changeDescription={this.changeDescription}
                />
            </>
        );
    }

    async fetchData() {
        const response = await fetch('https://localhost:5001/api/v2/product/get-all?skip=' + this.state.skip + '&take=' + this.state.take);
        const data = await response.json();
        const skipNext = this.state.skip + this.state.take;
        const newList = [...this.state.items, ...data];
        this.setState({ items: newList, skip: skipNext, endOfList: data.length === 0 });
    }

    async handleRowClick(productId) {
        const response = await fetch('https://localhost:5001/api/v2/product/get?productId=' + productId);
        const data = await response.json();

        this.setState({ isModalOpen: true, selectedProduct: data });
    }

    handleClose = () => {
        this.setState({ isModalOpen: false });
    }

    handleSave = () => {
        let formData = new FormData();
        formData.append('productId', this.state.selectedProduct.id);
        formData.append('newDescription', this.state.selectedProduct.description);

        axios.post('https://localhost:5001/api/v2/product/edit-description', formData)
            .then(function (response) {
                window.location.href = "/";
            })
            .catch(function (error) {
                console.log(error);
            });

        this.setState({ isModalOpen: false });
    }

    changeDescription = (e) => {
        var selected = this.state.selectedProduct;
        selected.description = e.target.value;

        this.setState({ selectedProduct: selected });
    }
}
