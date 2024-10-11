
function createCard(product)
{
    //Declaration section.

    const cardDiv = document.createElement("div");
    const imageContainerDiv = document.createElement("div");
    const textContainerDiv = document.createElement("div");
    const nameContainerdiv = document.createElement("div");
    const mainDiv = document.getElementById("main-div");
    const paragraphElement = document.createElement("p");
    const productImgElement = document.createElement("img");
    const priceContainerDiv = document.createElement("div");
    const ratingContainerDiv = document.createElement("div");
    const quantityContainerDiv = document.createElement("div");
    let isAvailableContainerDiv = document.createElement("div");
    const anchorPageRedirect = document.createElement("a");

    //Appending section.

    mainDiv.appendChild(cardDiv);
    cardDiv.appendChild(nameContainerdiv);
    cardDiv.appendChild(imageContainerDiv);
    cardDiv.appendChild(textContainerDiv);
    cardDiv.appendChild(priceContainerDiv);
    cardDiv.appendChild(quantityContainerDiv);
    cardDiv.appendChild(isAvailableContainerDiv);
    cardDiv.appendChild(ratingContainerDiv);
    
    //Class attribution section.

    cardDiv.className = "card-div";
    imageContainerDiv.className = "image-container";
    textContainerDiv.className = "text-container";
    nameContainerdiv.className = "name-container";
    priceContainerDiv.className = "price-container";
    ratingContainerDiv.className = "rating-container";
    quantityContainerDiv.className = "quantity-container";
    isAvailableContainerDiv.className = "availability-check";
    anchorPageRedirect.className = "product-anchor";

    //This section is affecting how the images are displayed and its behavior.

    productImgElement.src = product.imgUrl;
    productImgElement.setAttribute("width","100%");
    productImgElement.setAttribute("height","100%");
    imageContainerDiv.appendChild(productImgElement);
    anchorPageRedirect.href = `http://127.0.0.1:3000/product.html?id=${product.id}`;
    anchorPageRedirect.appendChild(productImgElement);
    imageContainerDiv.appendChild(anchorPageRedirect);

    /*This section is used to assign each object property to the inner text 
    of the corresponding element. As well as symbols and a boolean variable
    to evaluate weather the products are available or not.*/

    /*TO DO: Add a behavior to the star rating container, so when the user hovers
    over the stars */ 

    nameContainerdiv.innerText = product.name;
    paragraphElement.innerText = product.description;
    priceContainerDiv.innerText = "R$ "+product.price;
    textContainerDiv.appendChild(paragraphElement); 
    isAvailableContainerDiv = product.isAvailable;
    ratingContainerDiv.innerText = "★ ★ ★ ★ ★";
    
    /*"If" statement declared to test if the variable "isAvailable" is true based 
    on the quantity of products in stock */

    if(product.quantity > 0)
    {
        product.isAvailable = true;
        quantityContainerDiv.innerText = product.quantity+" in stock";

    }
    else
    {
        product.isAvailable = false;
        quantityContainerDiv.innerText = "This product is not available at the moment";
    }
       
}

