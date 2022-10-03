import React from 'react';
import { IBabyName } from '~/models';
import cx from 'classnames';
import styles from './NameCard.css';

export interface INameCardProps {
	name: IBabyName;
}

export function NameCard({ name }: INameCardProps) {
	return <div className={cx(styles.root, styles.blue)}>{name.name}</div>;
}
