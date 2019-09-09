import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { WorkItem } from './components/WorkItem';

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