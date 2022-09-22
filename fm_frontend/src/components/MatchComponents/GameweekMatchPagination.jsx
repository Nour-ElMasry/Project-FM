import React from 'react';
import IconButton from '@mui/material/IconButton';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';

const GameweekMatchPagination = (prop) => {
    return <div className='gameweekPagination flex flex-ai-c flex-jc-sb'>
        <IconButton 
            aria-label="backwardPage"
            sx={{borderRadius: '0'}}
            onClick={() => {
                if(!prop.pageLoading){
                    if(parseInt(prop.page) > 1){
                        prop.setPageLoading(true);
                        prop.setPage(parseInt(prop.page) - 1)
                    }
                }
            }}>
            <ChevronLeftIcon />
        </IconButton>
        
        <p>Gameweek {prop.page}</p>

        <IconButton 
            aria-label="forwardPage" 
            sx={{borderRadius: '0'}}
            onClick={() => {
                if(!prop.pageLoading){
                    if(parseInt(prop.page) < parseInt(prop.maxPages)){
                        prop.setPageLoading(true);
                        prop.setPage(parseInt(prop.page) + 1)
                    }  
                }
            }}
            >
            <ChevronRightIcon />
        </IconButton>
    </div>
}

export default GameweekMatchPagination;