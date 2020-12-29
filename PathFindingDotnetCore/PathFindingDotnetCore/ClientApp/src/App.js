import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Home from './components/GridManager/Home';
import PathFindingVisualiser from './components/PathFindingVisualiser/PathFindingVisualiser'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/Visualiser' component={PathFindingVisualiser} />
      </Layout>
    );
  }
}
