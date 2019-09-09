import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { WorkItem } from './components/WorkItem';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.min.css';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={WorkItem} />
            </Layout>
        );
    }
}