// third-party
import { PopupProps } from 'react-map-gl/maplibre';

// material-ui
import { Theme, SxProps } from '@mui/material/styles';

// project-import
import PopupStyled from './PopupStyled';

interface Props extends PopupProps {
  sx?: SxProps<Theme>;
  latitude: number;
  longitude: number;
  closeButton?: boolean;
  children?: React.ReactNode;
  onClose?: () => void;
}

// ==============================|| MAP BOX - MODAL ||============================== //

export default function MapPopup({ sx, children, ...other }: Props) {
  return (
    <PopupStyled anchor="bottom" sx={sx} {...other}>
      {children}
    </PopupStyled>
  );
}
