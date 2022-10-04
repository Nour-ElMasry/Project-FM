import Tooltip from '@mui/material/Tooltip';
import IconButton from '@mui/material/IconButton';
import Toolbar from '@mui/material/Toolbar';
import FilterListIcon from '@mui/icons-material/FilterList';
import PlayreFilterDialog from './PlayerFilterDialog';
import { useState } from 'react';

const FilterToolBar = (props) => {
    const [openFilter, setOpenFilter] = useState(false);
    
    const handleClickOpen = () => {
        setOpenFilter(true);
    };
  
    const handleClose = () => {
        setOpenFilter(false);
    };

    return <>
    <PlayreFilterDialog team={props.team}  openFilter={openFilter} handleClose={handleClose} handleFilterSubmit={props.handleFilterSubmit} />
    <Toolbar className='flex flex-ai-c flex-jc-sb'>
        <h4>Players List</h4>
        <Tooltip title="Filter list">
            <IconButton onClick={handleClickOpen}>
                <FilterListIcon />
            </IconButton>
        </Tooltip>
    </Toolbar>
</>
}

export default FilterToolBar;