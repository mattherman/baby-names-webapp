import { useSelector, TypedUseSelectorHook } from 'react-redux';
import { IBabyNamesState } from '~/names/names.slice';

export interface IState {
	names: IBabyNamesState;
}

export const useAppSelector: TypedUseSelectorHook<IState> = useSelector;
