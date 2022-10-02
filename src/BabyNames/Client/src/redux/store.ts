import { configureStore } from '@reduxjs/toolkit';
import { IState } from './IState';
import { rootReducer } from './rootReducer';

export function createStore() {
	return configureStore<IState>({
		reducer: rootReducer
	});
}
