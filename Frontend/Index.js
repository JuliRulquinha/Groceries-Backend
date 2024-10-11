let product = 
{
    "name": String,
    "imgUrl": String,
    "description": String,
    "price": Number,
    "quantity": Number,
    "isAvailable": true
}
  
let idGenerator = 0;
let products = [];

fetch("https://localhost:7226/Products/GetAllProducts")
.then(response => response.json())
.then(productsReceived => {
        products = productsReceived;
        
        for(let product of products)
        {
            createCard(product);
        }
});



function reorder(event)
{
    console.log(event.target.value);

    switch(event.target.value)
    {
        case "highest price":
            products.sort( (a,b) => b.price - a.price);
        break;

        case "lowest price":
            products.sort( (a,b) => a.price - b.price);
        break;

        case "name":
            products.sort((a,b) => 
                {
                    if(a.name > b.name)
                        return 1;
                    else if(a.name < b.name)
                        return -1;
                });
        break;
    }
    
    clearAll();

    for(let product of products)
    {
        createCard(product);
    }
        
}

function clearAll()
{
    let mainDiv = document.getElementById("main-div");
    mainDiv.innerHTML = "";
}

function addProduct()
{
    
    const Name = document.getElementById("p-name").value;
    const ImgUrl = document.getElementById("p-img").value;
    const Description = document.getElementById("p-description").value;
    const Price = document.getElementById("p-price").value;
    const Quantity = document.getElementById("p-quantity").value;

    const body ={
        Name,
        ImgUrl,
        Description,
        Price,
        Quantity
    }
    fetch("https://localhost:7226/Products/Products",{
        method: "POST",
        headers: {
            "Content-Type": "application/json"

        },
        body:JSON.stringify(body)
    }).then(response => response.json())
    .then(data =>{
            console.log(data);
    })
        
}

    
    


let form = document.getElementById("product-form");

form.addEventListener('submit', function(event) {
    // Prevent the default form submission
    event.preventDefault();

    // Custom code to handle the form submission
    console.log('Form submission prevented.');
});


function clearForm()
{
   
    form.reset();
}

function createProductPage()
{

}

