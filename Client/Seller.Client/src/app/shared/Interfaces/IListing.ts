export interface IListing {
    id: string,
    title: string,
    imageUrl: string,
    price: number,
    description?: string,
    created?: string,
    offersCount?: string
    sellerId?: string,
    sellerName?: string,
    isDeleted?: boolean
  }