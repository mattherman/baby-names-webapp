import { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { useAppDispatch } from './redux';
import sessionSlice from './session/session.slice';

function Heartbeat(): JSX.Element {
	const dispatch = useAppDispatch();
	const { refreshToken } = bindActionCreators(sessionSlice.actions, dispatch);
	useEffect(() => {
		const intervalId = window.setInterval(refreshToken, 60 * 1000);

		return () => {
			window.clearInterval(intervalId);
		};
	});

	return null;
}

export default Heartbeat;
