import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { PathFindingVisualiser } from './components/PathFindingVisualiser/PathFindingVisualiser';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={PathFindingVisualiser} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
