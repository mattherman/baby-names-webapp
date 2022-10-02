import { configureStore } from '@reduxjs/toolkit';
import { IState } from './state';
import { rootReducer } from './rootReducer';

export const store = configureStore<IState>({
	reducer: rootReducer,
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
