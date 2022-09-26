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
                    if(prop.page > 1){
                        prop.setPageLoading(true);
                        sessionStorage.setItem("Page_Key", prop.page - 1)
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
                    if(prop.page < prop.maxPages){
                        prop.setPageLoading(true);
                        sessionStorage.setItem("Page_Key", prop.page + 1)
                        prop.setPage(prop.page + 1)
                    }  
                }
            }}
            >
            <ChevronRightIcon />
        </IconButton>
    </div>
}

export default GameweekMatchPagination;